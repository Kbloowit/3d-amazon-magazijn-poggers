class Forklift extends THREE.Group {
    constructor() {
        super();
        this.init();

    }

    init() {
        var selfref = this;
        LoadOBJModel('textures/', 'forklift.obj', 'textures/', 'forklift.mtl', mesh => {
            selfref.add(mesh);
        });
        selfref.scale.set(4, 4, 4);



    }

}
