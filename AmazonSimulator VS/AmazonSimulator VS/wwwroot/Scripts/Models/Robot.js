﻿/** Creates robot model*/
class Robot extends THREE.Group {
    constructor() {
        super();
        this.init();
    }

    /** Initializes the robot model */
    init() {
        var selfref = this;
        var geometryRobot = new THREE.BoxGeometry(0.9, 0.3, 0.9);

        var cubeMaterialsRobot = [
            new THREE.MeshPhongMaterial({
                map: new THREE.TextureLoader().load('textures/ThreeDModels/robot/robot_side.png'),
                side: THREE.DoubleSide
            }),
            //LEFT
            new THREE.MeshPhongMaterial({
                map: new THREE.TextureLoader().load('textures/ThreeDModels/robot/robot_side.png'),
                side: THREE.DoubleSide
            }),
            //RIGHT
            new THREE.MeshPhongMaterial({
                map: new THREE.TextureLoader().load('textures/ThreeDModels/robot/robot_top.png'),
                side: THREE.DoubleSide
            }),
            //TOP
            new THREE.MeshPhongMaterial({
                map: new THREE.TextureLoader().load('textures/ThreeDModels/robot/robot_bottom.png'),
                side: THREE.DoubleSide
            }),
            //BOTTOM
            new THREE.MeshPhongMaterial({
                map: new THREE.TextureLoader().load('textures/ThreeDModels/robot/robot_front.png'),
                side: THREE.DoubleSide
            }),
            //FRONT
            new THREE.MeshPhongMaterial({
                map: new THREE.TextureLoader().load('textures/ThreeDModels/robot/robot_front.png'),
                side: THREE.DoubleSide
            })
        ];

        //var materialRobot = new THREE.MeshFaceMaterial(cubeMaterialsRobot);
        var modelRobot = new THREE.Mesh(geometryRobot, cubeMaterialsRobot);
        modelRobot.position.y = 0.15;

        selfref.add(modelRobot);
    }
}