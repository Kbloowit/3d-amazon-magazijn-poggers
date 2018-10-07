/** Creates shelf model*/
class Shelf extends THREE.Group {
    constructor() {
        super();
        this.init();
    }

    /** Initializes the shelf model */
    init() {
        var selfref = this;

        LoadOBJModel('textures/ThreeDModels/shelf/', 'shelf.obj', 'textures/ThreeDModels/shelf/', 'shelf.mtl', mesh => {
            var box = new THREE.Box3().setFromObject(mesh);
            box.center(mesh.position); // this re-sets the mesh position
            mesh.position.y = 0;
            mesh.position.multiplyScalar(- 1);
            mesh.castShadow = true;
            selfref.add(mesh);
        });

        selfref.scale.set(0.3, 0.3, 0.3);
    }
}