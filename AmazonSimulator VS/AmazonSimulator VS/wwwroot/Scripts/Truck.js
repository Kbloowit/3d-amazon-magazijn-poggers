/** Creates truck model*/
class Truck extends THREE.Group {
    constructor() {
        super();
        this.init();
    }
    /** Initializes the truck model */
    init() {
        var selfref = this;

        LoadOBJModel('textures/ThreeDModels/truck/', 'truck.obj', 'textures/ThreeDModels/truck/', 'truck.mtl', mesh => {
            mesh.scale.set(30, 30, 30);
            selfref.add(mesh);
        });

        //Add light to the truck
        AddPointLight(selfref, 0xffffff, 1, 2, -3.45, -0.3, -0.8);
        AddPointLight(selfref, 0xffffff, 1, 2, -3.45, -0.3, 0.8);
        AddPointLight(selfref, 0xff0000, 1, 2, 3.10, -0.3, -0.8);
        AddPointLight(selfref, 0xff0000, 1, 2, 3.10, -0.3, 0.8);
        AddSpotLight(selfref, 0xffffff, 1, 50, 1, Math.PI / 6, - 2.69, -0.3, -0.8, -7.45, -0.3, 0.8);
        AddSpotLight(selfref, 0xffffff, 1, 50, 1, Math.PI / 6, -2.69, -0.3, 0.8, -7.45, -0.3, 0.8);
    }
}