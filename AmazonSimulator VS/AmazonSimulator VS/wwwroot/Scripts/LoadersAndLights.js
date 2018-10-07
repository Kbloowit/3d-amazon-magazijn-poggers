/**
 * Loads .obj and .mtl files
 * @param {any} modelPath path to obj file
 * @param {any} modelName obj file
 * @param {any} texturePath path to mtl file
 * @param {any} textureName mtl file
 * @param {any} onload Mesh to load
 */
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

/**
 * Creates a pointlight in the world
 * @param {any} object object the light is attached to
 * @param {any} colour color of the light
 * @param {any} intensity intensity of the light
 * @param {any} distance distance the light shines
 * @param {any} x position X of the light
 * @param {any} y position y of the light
 * @param {any} z position z of the light
 */
function AddPointLight(object, colour, intensity, distance, x, y, z) {
    var Light = new THREE.PointLight(colour, intensity, distance);
    Light.position.set(x, y, z);
    Light.decay = 2;
    object.add(Light);
}

/**
 * Creates a spotlight in the world
 * @param {any} object object the light is attached to
 * @param {any} colour color of the light
 * @param {any} intensity intensity of the light
 * @param {any} distance distance the light shines
 * @param {any} penumbra percentage of the lit area that is the brightest
 * @param {any} angle angle the light is pointing
 * @param {any} x position x of the light
 * @param {any} y position y of the light
 * @param {any} z position z of the light
 * @param {any} tx target x cordinate the light is pointing to
 * @param {any} ty target y cordinate the light is pointing to
 * @param {any} tz target z cordinate the light is pointing to
 */
function AddSpotLight(object, colour, intensity, distance, penumbra, angle, x, y, z, tx, ty, tz,) {
    var Light = new THREE.SpotLight(colour, intensity, distance, angle, penumbra);
    Light.castShadow = true;
    Light.position.set(x, y, z);
    Light.decay = 2;
    object.add(Light);

    object.add(Light.target);
    Light.target.position.set(tx, ty, tz);
}