using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static курсовой_бактерии.MainWindow;

namespace TestProject_kyr
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Class_Bacterium_Method_Bacterium0()
        {
            GenCode testGen = new GenCode();

            Bacterium test = new Bacterium(0);
            testGen = test.GetGenForTesting;

            Assert.IsNotNull(test);
            Assert.IsTrue(
                (testGen.rgb1 >= 0) && (testGen.rgb1 <= 240) &&
                (testGen.rgb2 >= 0) && (testGen.rgb2 <= 240) &&
                (testGen.rgb3 >= 0) && (testGen.rgb3 <= 240) &&
                (testGen.vector >= 0) && (testGen.vector <= 8) &&
                (testGen.distance >= 0) && (testGen.distance <= 1000) &&
                (testGen.seconds >= 1) && (testGen.seconds <= 3) && (testGen.NumChild == 2)
                );
        }
        [TestMethod]
        public void Class_Bacterium_Method_Bacterium1()
        {
            Random random = new Random();
            GenCode testGen = new GenCode();
            GenCode gen = new GenCode();

            gen.rgb1 = random.Next(0, 240);
            gen.rgb2 = random.Next(0, 240);
            gen.rgb3 = random.Next(0, 240);
            gen.vector = random.Next(0, 8);
            gen.distance = random.Next(0, 1000);
            gen.seconds = random.Next(1, 3);
            gen.NumChild = 2;

            Bacterium test = new Bacterium(gen, 0);
            testGen = test.GetGenForTesting;

            Assert.IsNotNull(test);
            Assert.IsTrue(
                (testGen.rgb1 >= 0) && (testGen.rgb1 <= 240) &&
                (testGen.rgb2 >= 0) && (testGen.rgb2 <= 240) &&
                (testGen.rgb3 >= 0) && (testGen.rgb3 <= 240) &&
                (testGen.vector >= 0) && (testGen.vector <= 8) &&
                (testGen.distance >= 0) && (testGen.distance <= 1000) &&
                (testGen.seconds >= 1) && (testGen.seconds <= 3) && 
                (testGen.NumChild >= 0) && (testGen.NumChild <=3)
                );
        }
    }
}
