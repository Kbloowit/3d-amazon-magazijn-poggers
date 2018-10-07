/** Creates forklift model*/
class Forklift extends THREE.Group {
    constructor() {
        super();
        this.init();
    }

    /** Initializes the forklift model */
    init() {
        var selfref = this;

        LoadOBJModel('textures/ThreeDModels/forklift/', 'forklift.obj', 'textures/ThreeDModels/forklift/', 'forklift.mtl', mesh => {
            var box = new THREE.Box3().setFromObject(mesh);
            box.center(mesh.position); // this re-sets the mesh position
            mesh.position.y = 0.1;
            mesh.position.z = -0.2;
            mesh.position.x = -0.1;
            selfref.add(mesh);
        });

        selfref.scale.set(6, 6, 6);
    }
}