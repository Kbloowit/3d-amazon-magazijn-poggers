class WorldStandards extends THREE.Group {
    constructor() {
        super();
        this.init();
    }

    init() {
        var selfref = this;

        LoadOBJModel('textures/attempt/', 'attempt.obj', 'textures/attempt/', 'attempt.mtl', mesh => {
            mesh.rotation.y = Math.PI / 2.0;
            mesh.position.y = 0;
            mesh.position.x = 4;
            mesh.position.z = 0.85;
            selfref.add(mesh);
        });
        selfref.scale.set(4, 4, 4);
    }
}
