class Shelf extends THREE.Group {
    constructor() {
        super();
        this.init();

    }

    init() {
        var selfref = this;
        LoadOBJModel('textures/shelf/', 'shelf.obj', 'textures/shelf/', 'shelf.mtl', mesh => {
            var box = new THREE.Box3().setFromObject(mesh);
            box.center(mesh.position); // this re-sets the mesh position
            mesh.position.y = 0;
            mesh.position.multiplyScalar(- 1);
            selfref.add(mesh);
        });
        selfref.scale.set(0.3, 0.3, 0.3);



    }

}


    

