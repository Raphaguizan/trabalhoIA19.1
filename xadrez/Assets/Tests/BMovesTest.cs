using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class BMovesTest {

    [Test]
    public void Test01() {
        //GameObject[][] g = createGameObject();
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "White Queen";
        Assert.AreEqual("White Queen", cube.name);
    }
    
}
