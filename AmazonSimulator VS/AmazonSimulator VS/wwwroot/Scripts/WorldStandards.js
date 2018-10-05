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
        AddPointLight(selfref, 0x00ffff, 1, 2, 4, 2.5, 4);
        AddSpotLight(selfref, 0xff0000, 1, 50, 1, Math.PI / 6, 4, 2.5, 4, 4, 0, 4);
        AddSpotLight(selfref, 0xff0000, 1, 50, 1, Math.PI / 6, 2, 2.5, 4, 1, 0, 4);
        AddSpotLight(selfref, 0xff0000, 1, 50, 1, Math.PI / 6, 7, 2.5, 4, 7, 0, 4);

    }
}
