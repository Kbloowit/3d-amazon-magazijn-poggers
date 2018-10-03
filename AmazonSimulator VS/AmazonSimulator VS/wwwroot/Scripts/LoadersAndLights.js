function LoadOBJModel(modelPath, modelName, texturePath, textureName, onload) {
    new THREE.MTLLoader().setPath(texturePath).load(textureName, function (materials) {
        materials.preload();
        new THREE.OBJLoader().setPath(modelPath).setMaterials(materials).load(modelName, function (object) {
            onload(object);
        }, function () {
        }, function (e) {
            console.log('Error loading model');
            console.log(e);
        });
    });
}

function AddPointLight(object, colour, intensity, distance, x, y, z) {

    var Light = new THREE.PointLight(colour, intensity, distance);
    Light.position.set(x, y, z);
    Light.decay = 2;
    object.add(Light);

    //var box = new THREE.Mesh(new THREE.BoxGeometry(1, 0.8, 0.8), new THREE.MeshBasicMaterial(0xffffff));
    //box.position.set(x, y, z);
    //object.add(box);




}
function AddSpotLight(object, colour, intensity, distance, x, y, z, tx, ty, tz) {
    var Light = new THREE.SpotLight(colour, intensity, distance);
    Light.castShadow = true;
    Light.position.set(x, y, z);
    Light.decay = 2;
    object.add(Light);

    object.add(Light.target);
    Light.target.position.set(tx, ty, tz);



}