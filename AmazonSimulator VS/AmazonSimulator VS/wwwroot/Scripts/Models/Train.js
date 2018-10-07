/** Creates train model*/
class Train extends THREE.Group {
    constructor() {
        super();
        this.init();
    }

    /** Initializes the train model */
    init() {
        var selfref = this;

        LoadOBJModel('textures/ThreeDModels/train/', 'train.obj', 'textures/ThreeDModels/train/', 'train.mtl', mesh => {
            selfref.add(mesh);
        });

        selfref.scale.set(8, 8, 8);
    }
}