using StereoKit;
using System;

namespace Extinctverse
{
    class Program
    {
        /// Variables
        static Mesh floorMesh;
        static Material floorMat;
        static int buttonSwitchCount = 0;
        static Pose windowPoseButton = new Pose(-1, 0, 0, Quat.LookDir(1,0,-0.5f));

        static Model room = Model.FromFile("circular_showroom/scene.gltf");

        static Model wooly_mammoth = Model.FromFile("wooly_mammoth/scene.gltf");
        static Model tasmanian_tiger = Model.FromFile("tasmanian_tiger/source/NHMW-NMW-ST132_Beutelwolf_low res.obj");
        static Model blue_antelope = Model.FromFile("blue_antelope/source/NHMW-Zoo1-MAMM St715_Hippotragus leucophaeus_Blaubock_low res.obj"); 
        static Model quaga = Model.FromFile("quaga/source/NHMW-Zoo1-MAMM St710_Eguus quagga_Quagga_low res.obj");
        static Model dodo = Model.FromFile("dodo-model/scene.gltf");
        static Model auk = Model.FromFile("geirfugl/scene.gltf");
        static Model cuban_macaw = Model.FromFile("cuban_macaw/source/NHMW-Zoo1-Vofgel-50796-Ara tricolor_low res.obj");
        static Model norfolk_kaka = Model.FromFile("norfolk_kaka/scene.gltf");
        static Model trex = Model.FromFile("trex/scene.gltf");
        static Model velociraptor = Model.FromFile("velociraptor/scene.gltf");
        static Model styracosaurus = Model.FromFile("styracosaurus/scene.gltf");

        static void Main(string[] args)
        {
            // Initialize StereoKit
            SKSettings settings = new SKSettings
            {
                appName = "Extinctverse",
                assetsFolder = "Assets",
            };
            if (!SK.Initialize(settings))
                Environment.Exit(1);

            Matrix floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
            Material floorMaterial = new Material(Shader.FromFile("floor.hlsl"));
            floorMaterial.Transparency = Transparency.Blend;

            InitializeFloor();
            InitMaterials();

            // Core application loop
            while (SK.Step(() =>
            {
                //  if (SK.System.displayType == Display.Opaque)
                // Default.MeshCube.Draw(floorMaterial, floorTransform);
                //  floorMesh?.Draw(floorMat, Matrix.T(0, -1.5f, 0));
                room.Draw(Matrix.TS(new Vec3(0, -1.5f, 0), 1f));

                ShowSwitchButtonUI();
                ShowModels();
            })) ;
            SK.Shutdown();
        }

        static void InitializeFloor()
        {
            // Add a floor if we're in VR
            floorMesh = Mesh.GeneratePlane(new Vec2(10, 10));
            floorMat = Default.Material.Copy();
            floorMat[MatParamName.DiffuseTex] = Tex.FromFile("floor1.jpg");
            floorMat[MatParamName.TexScale] = 8;
        }

        static void InitMaterials()
        {
            Material cuban_macaw_mat = Material.Unlit.Copy();
            cuban_macaw_mat[MatParamName.DiffuseTex] = Tex.FromFile("cuban_macaw/textures/material_0_diffuse.png");
            cuban_macaw.SetMaterial(0, cuban_macaw_mat);

            Material tasmanian_tiger_mat = Material.Unlit.Copy();
            tasmanian_tiger_mat[MatParamName.DiffuseTex] = Tex.FromFile("tasmanian_tiger/textures/material_0_diffuse.png");
            tasmanian_tiger.SetMaterial(0, tasmanian_tiger_mat);

            Material blue_antelope_mat = Material.Unlit.Copy();
            blue_antelope_mat[MatParamName.DiffuseTex] = Tex.FromFile("blue_antelope/textures/material_0_diffuse.png");
            blue_antelope.SetMaterial(0, blue_antelope_mat);

            Material quagaMat = Default.Material.Copy();
            quagaMat[MatParamName.DiffuseTex] = Tex.FromFile("quaga/textures/material_0_diffuse.png");
            quaga.SetMaterial(0, quagaMat);
        }

        static void ShowSwitchButtonUI()
        {
            UI.WindowBegin("", ref windowPoseButton, new Vec2(30, 0) * U.cm, false ? UIWin.Normal : UIWin.Body);
            UI.ColorScheme = Color.White;
            string btnNextText = "Next", btnPreviousText = "Previous";
            string animalName = "";
            string animalInfo = "";

            if (UI.Button(btnPreviousText))
            {
                if (buttonSwitchCount > 0)
                {
                    buttonSwitchCount--;
                }
                else
                {
                    buttonSwitchCount = 10;
                }
            }

            UI.SameLine();

            if (UI.Button(btnNextText))
            {
                if (buttonSwitchCount < 10)
                {
                    buttonSwitchCount++;
                }
                else
                {
                    buttonSwitchCount = 0;
                }
            }

            switch (buttonSwitchCount)
            {
                case 0:
                    animalName = "Woolly Mammoth";
                    animalInfo = "The woolly mammoth(Mammuthus primigenius) was an enormous mammal that once roamed the vast frozen," +
                        " northern landscapes in large size. Believed to be closely related to the modern-day elephant," +
                        " the woolly mammoth was an herbivore and remained in the wild until roughly 1700 BC when it became extinct.";
                    break;
                case 1:
                    animalName = "Tasmanian Tiger";
                    animalInfo = "The thylacine(Thylacinus cynocephalus) is an extinct carnivorous marsupial which was a slender fox-faced animal that was native to the Australian mainland and " +
                        "the islands of Tasmania and New Guinea. The last known live animal was captured in 1930 in Tasmania. " +
                        "It is commonly known as the Tasmanian tiger or the Tasmanian wolf.";
                    break;
                case 2:
                    animalName = "Quagga";
                    animalInfo = "The quagga(Equus quagga quagga) is an extinct subspecies of the plains zebra.The last animal died in the Zoo in Amsterdam in 1883." +
                        "Quaggas were endemic to southern and western Africa and became extinct in the wild in the second half of the 19th century. ";
                    break;
                case 3:
                    animalName = "Bluebuck";
                    animalInfo = "The bluebuck or blue antelope (Hippotragus leucophaeus) is an extinct species of antelope that lived in South Africa until around 1800." +
                                            "Europeans encountered the Bluebuck in the 17th century, but it was already uncommon by then. European settlers hunted it avidly, despite its flesh being distasteful, " +
                                            "while converting its habitat to agriculture. The Bluebuck became extinct around 1800"; break;
                case 4:
                    animalName = "Dodo";
                    animalInfo = "dodo, (Raphus cucullatus), extinct flightless bird of Mauritius (an island of the Indian Ocean)." +
                        " The birds were first seen by Portuguese sailors about 1507 and were exterminated by humans and their introduced animals." +
                        " The dodo was extinct by 1681. The dodo is frequently cited as one of the most well-known examples of human-induced extinction" +
                        " and also serves as a symbol of obsolescence with respect to human technological progress.";
                    break;
                case 5:
                    animalName = "Great auk";
                    animalInfo = "The great auk (Pinguinus impennis) is a species of flightless alcid that became extinct in the mid-19th century. It was the only modern species in the genus Pinguinus. " +
                        "In the past, the Great Auk was found in great numbers on islands off eastern Canada, Greenland, Iceland, Norway, Ireland and Great Britain, but it was eventually hunted to extinction." +
                        "Remains found in Floridan middens suggest that at least occasionally, birds ventured that far south in winter, possibly as recently as in the 17th century";
                    break;
                case 6:
                    animalName = "Cuban macaw";
                    animalInfo = "The Cuban macaw or Cuban red macaw (Ara tricolor) was a species of macaw native to the main island of Cuba and the nearby Isla de la Juventud that became extinct in the late 19th century." +
                        " Its extinction was caused by deforestation, hunting for food and for the pet trade.";
                    break;
                case 7:
                    animalName = "Norfolk Kaka";
                    animalInfo = "The Norfolk kaka (Nestor productus) is an extinct species of large parrot, belonging to the parrot family Nestoridae. " +
                        "The birds were about 38 cm long, with mostly olive-brown upperparts, (reddish-)orange cheeks and throat, straw-coloured breast, thighs, rump and lower abdomen dark orange and a prominent beak. " +
                        "It inhabited the rocks and treetops of Norfolk Island and adjacent Phillip Island. It was a relative of the New Zealand kaka.The last bird in captivity died in London in 1851.";
                    break;
                case 8:
                    animalName = "Tyrannosaurus rex";
                    animalInfo = "One of the largest known carnivorous dinosaurs, Tyrannosaurus rex — T. rex, for short — is also arguably the most iconic.In fact, the animal's name means king of the tyrant lizards." +
                        "T. rex was a member of the Tyrannosauroidea family of huge predatory dinosaurs with small arms and two-fingered hands.T. rex was a huge carnivore and primarily ate herbivorous dinosaurs, " +
                        "including Edmontosaurus and Triceratops. The predator acquired its food through scavenging and hunting, grew incredibly fast and ate hundreds of pounds at a time.";
                    break;
                case 9:
                    animalName = "Velociraptor";
                    animalInfo = "Velociraptor(genus Velociraptor), sickle-clawed dinosaur that flourished in central and eastern Asia duringis. It is a genus of dromaeosaurid theropod dinosaur that lived approximately" +
                        " 75 to 71 million years ago during the latter part of the Cretaceous Period.Velociraptor appears to have been a swift, agile predator of small herbivores.";
                    break;
                case 10:
                    animalName = "Styracosaurus";
                    animalInfo = "Styracosaurus lived 75 million years ago on a vast coastal plain in what is now Alberta, Canada and Montana, USA.It is a social animal," +
                        " Styracosaurus must forms pairs or small herds between two and five individuals to satisfy its comfort requirements. They can tolerate a fair number of other dinosaurs, " +
                        "making it ideal to mix them with other ceratopsians or unrelated species. Additionally, Styracosaurus is capable of living somewhat peacefully alongside small carnivores such as Deinonychus and Dilophosaurus," +
                        " only occasionally engaging in non-fatal territorial fights.";
                    break;
            }

            UI.Label(animalName);
            UI.HSeparator();
            UI.Text(animalInfo, TextAlign.TopCenter);
            UI.WindowEnd();
        }

        static void ShowModels()
        {
            // (lr,updown,nearfar)
            switch (buttonSwitchCount)
            {
                case 0:
                    wooly_mammoth.Draw(Matrix.TS(new Vec3(-1f, -1.325f, 4), 2.2f));
                    break;
                case 1:
                    tasmanian_tiger.Draw(Matrix.TRS(new Vec3(-0.075f, -0.7f, 1.2f), Quat.FromAngles(-90, 270, 0), 0.001175f));
                    break;
                case 2:
                    quaga.Draw(Matrix.TRS(new Vec3(-0.4f, -0.7f, 1.75f), Quat.FromAngles(-90, 180, 0), 0.0009f));
                    break;
                case 3:
                    blue_antelope.Draw(Matrix.TRS(new Vec3(0.4f, -0.08f, 1.7f), Quat.FromAngles(-90, -90, 0), 0.00125f));
                    break;
                case 4:
                    dodo.Draw(Matrix.TS(new Vec3(0, -1.5f, 1.5f), 0.12f));
                    break;
                case 5:
                    auk.Draw(Matrix.TRS(new Vec3(0, -1.05f, 1), Quat.FromAngles(0, 250, 0), 1f));
                    break;
                case 6:
                    cuban_macaw.Draw(Matrix.TRS(new Vec3(0.2f, -1, 1), Quat.FromAngles(0, 90, 0), 0.0017f));
                    break;
                case 7:
                    norfolk_kaka.Draw(Matrix.TS(new Vec3(-3.5f, -4.7f, -3), 1.1f));
                    break;
                case 8:
                    trex.Draw(Matrix.TRS(new Vec3(-4, -153f, 3), Quat.FromAngles(0, 30, 0), 40f));
                    break;
                case 9:
                    velociraptor.Draw(Matrix.TRS(new Vec3(0, -0.5f, 1.65f), Quat.FromAngles(0, -45, 0), 0.35f));
                    break;
                case 10:
                    styracosaurus.Draw(Matrix.TRS(new Vec3(-1f, -0.3f, 2.65f), Quat.FromAngles(0, -75, 0), 1f));
                    break;
            }
        }
    }
}
