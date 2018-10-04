class Forklift extends THREE.Group {
    constructor() {
        super();
        this.init();

    }

    init() {
        var selfref = this;
        LoadOBJModel('textures/forklift/', 'forklift.obj', 'textures/forklift/', 'forklift.mtl', mesh => {
            selfref.add(mesh);
        });
        selfref.scale.set(4, 4, 4);



    }

}
