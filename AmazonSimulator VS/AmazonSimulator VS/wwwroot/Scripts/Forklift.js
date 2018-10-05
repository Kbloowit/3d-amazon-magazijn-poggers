class Forklift extends THREE.Group {
    constructor() {
        super();
        this.init();
    }

    init() {
        var selfref = this;

        LoadOBJModel('textures/forklift/', 'forklift.obj', 'textures/forklift/', 'forklift.mtl', mesh => {
            var box = new THREE.Box3().setFromObject(mesh);
            box.center(mesh.position); // this re-sets the mesh position
            mesh.position.y = 0.1;
            mesh.position.z = -0.2;
            selfref.add(mesh);
        });

        selfref.scale.set(6, 6, 6);
    }
}
