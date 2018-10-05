class Train extends THREE.Group {
    constructor() {
        super();
        this.init();
    }

    init() {
        var selfref = this;

        LoadOBJModel('textures/train/', 'train.obj', 'textures/train/', 'train.mtl', mesh => {
            selfref.add(mesh);
        });

        selfref.scale.set(8, 8, 8);
    }
}