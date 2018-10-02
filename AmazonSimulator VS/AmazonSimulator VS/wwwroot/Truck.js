function Truck(scene, worldObjects, modelPath, modelName, texturePath, textureName) {
    var Truck = THREE.Group();
    LoadOBJModel(modelPath, modelName, texturePath, textureName, mesh => {
        mesh.scale.set(30, 30, 30);
        Truck.add(mesh);
    });
    AddPointLight(Truck, 0xffffff, 1, 2, -3.45, -0.3, -0.8);
    AddPointLight(Truck, 0xffffff, 1, 2, -3.45, -0.3, 0.8);
    AddPointLight(Truck, 0xff0000, 1, 2, 3.10, -0.3, -0.8);
    AddPointLight(Truck, 0xff0000, 1, 2, 3.10, -0.3, 0.8);
    AddSpotLight(Truck, 0xffffff, 1, 0, -2.69, -0.3, -0.8, -7.45, -0.3, 0.8);
    AddSpotLight(Truck, 0xffffff, 1, 0, -2.69, -0.3, 0.8, -7.45, -0.3, 0.8);


    scene.add(Truck);
    worldObjects[command.parameters.guid] = Truck;
};
