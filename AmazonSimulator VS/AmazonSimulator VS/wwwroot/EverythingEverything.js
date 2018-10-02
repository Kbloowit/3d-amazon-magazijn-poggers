﻿function parseCommand(input = "") {
    return JSON.parse(input);
}

var exampleSocket;

window.onload = function () {
    var camera, scene, renderer;
    var cameraControls;

    var worldObjects = {};

    function init() {
        camera = new THREE.PerspectiveCamera(70, window.innerWidth / window.innerHeight, 1, 1500);
        cameraControls = new THREE.OrbitControls(camera);
        camera.position.z = 15;
        camera.position.y = 5;
        camera.position.x = 15;
        cameraControls.update();
        scene = new THREE.Scene();

        renderer = new THREE.WebGLRenderer({ antialias: true });
        renderer.setPixelRatio(window.devicePixelRatio);
        renderer.setSize(window.innerWidth, window.innerHeight + 5);
        document.body.appendChild(renderer.domElement);

        window.addEventListener('resize', onWindowResize, false);

        var geometryPlane = new THREE.PlaneGeometry(32, 32, 32);
        var material = new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/floor.jpg"), side: THREE.DoubleSide });
        var plane = new THREE.Mesh(geometryPlane, material);
        plane.rotation.x = Math.PI / 2.0;
        plane.position.y = 0;
        plane.position.x = 15;
        plane.position.z = 15;
        scene.add(plane);

        var cubeSkyboxGeometry = new THREE.BoxGeometry(1000, 1000, 1000);
        var cubeSkyboxMaterial = [

            new THREE.MeshBasicMaterial({ map: new THREE.TGALoader().load("textures/mp_crimson/crimson-tide_lf.tga"), side: THREE.DoubleSide }), //LEFT
            new THREE.MeshBasicMaterial({ map: new THREE.TGALoader().load("textures/mp_crimson/crimson-tide_rt.tga"), side: THREE.DoubleSide }), //RIGHT
            new THREE.MeshBasicMaterial({ map: new THREE.TGALoader().load("textures/mp_crimson/crimson-tide_up.tga"), side: THREE.DoubleSide }), //TOP
            new THREE.MeshBasicMaterial({ map: new THREE.TGALoader().load("textures/mp_crimson/crimson-tide_dn.tga"), side: THREE.DoubleSide }), //BOTTOM
            new THREE.MeshBasicMaterial({ map: new THREE.TGALoader().load("textures/mp_crimson/crimson-tide_ft.tga"), side: THREE.DoubleSide }), //FRONT
            new THREE.MeshBasicMaterial({ map: new THREE.TGALoader().load("textures/mp_crimson/crimson-tide_bk.tga"), side: THREE.DoubleSide }) //BACK
        ];

        var cubeSkybox = new THREE.Mesh(cubeSkyboxGeometry, cubeSkyboxMaterial);
        scene.add(cubeSkybox);

        var light = new THREE.AmbientLight(0x404040);
        light.intensity = 2.5;
        scene.add(light);
    }

    function onWindowResize() {
        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();
        renderer.setSize(window.innerWidth, window.innerHeight);
    }

    function animate() {
        requestAnimationFrame(animate);
        cameraControls.update();
        renderer.render(scene, camera);
    }


    exampleSocket = new WebSocket('ws://' + window.location.hostname + ':' + window.location.port + '/connect_client');
    exampleSocket.onmessage = function (event) {
        var command = parseCommand(event.data);
        if (command.command === 'update') {
            if (Object.keys(worldObjects).indexOf(command.parameters.guid) < 0) {
                if (command.parameters.type === 'truck') {
                    //new Truck(scene, worldObjects, 'textures/', 'truck.obj', 'textures/', 'truck.mtl');

                    var Truck = new THREE.Group();
                    LoadOBJModel('textures/', 'truck.obj', 'textures/', 'truck.mtl', mesh => {
                        mesh.scale.set(30, 30, 30);
                        Truck.add(mesh);
                    });
                    AddPointLight(Truck, 0xffffff, 1, 2, -3.45, -0.3, -0.8);
                    AddPointLight(Truck, 0xffffff, 1, 2, -3.45, -0.3, 0.8);
                    AddPointLight(Truck, 0xff0000, 1, 2, 3.10, -0.3, -0.8);
                    AddPointLight(Truck, 0xff0000, 1, 2, 3.10, -0.3, 0.8);
                    AddSpotLight(Truck, 0xffffff, 1, 0, -2.69, -0.3, -0.8, -7.45, -0.3, 0.8)
                    AddSpotLight(Truck, 0xffffff, 1, 0, -2.69, -0.3, 0.8, -7.45, -0.3, 0.8)


                    scene.add(Truck);
                    worldObjects[command.parameters.guid] = Truck;
                }
                if (command.parameters.type === 'robot') {
                    var geometryRobot = new THREE.BoxGeometry(0.9, 0.3, 0.9);
                    var cubeMaterialsRobot = [
                        new THREE.MeshPhongMaterial({
                            map: new THREE.TextureLoader().load('textures/robot_side.png'),
                            side: THREE.DoubleSide
                        }),
                        //LEFT
                        new THREE.MeshPhongMaterial({
                            map: new THREE.TextureLoader().load('textures/robot_side.png'),
                            side: THREE.DoubleSide
                        }),
                        //RIGHT
                        new THREE.MeshPhongMaterial({
                            map: new THREE.TextureLoader().load('textures/robot_top.png'),
                            side: THREE.DoubleSide
                        }),
                        //TOP
                        new THREE.MeshPhongMaterial({
                            map: new THREE.TextureLoader().load('textures/robot_bottom.png'),
                            side: THREE.DoubleSide
                        }),
                        //BOTTOM
                        new THREE.MeshPhongMaterial({
                            map: new THREE.TextureLoader().load('textures/robot_front.png'),
                            side: THREE.DoubleSide
                        }),
                        //FRONT
                        new THREE.MeshPhongMaterial({
                            map: new THREE.TextureLoader().load('textures/robot_front.png'),
                            side: THREE.DoubleSide
                        })
                    ];
                    var materialRobot = new THREE.MeshFaceMaterial(cubeMaterialsRobot);
                    var modelRobot = new THREE.Mesh(geometryRobot, materialRobot);
                    modelRobot.position.y = 0.15;
                    var robotGroup = new THREE.Group();
                    robotGroup.add(modelRobot);
                    scene.add(robotGroup);
                    worldObjects[command.parameters.guid] = robotGroup;
                }
                if (command.parameters.type === 'shelf') {
                    var Shelf = new THREE.Group();
                    LoadOBJModel('textures/', 'shelf.obj', 'textures/', 'shelf.mtl', mesh => {
                        var box = new THREE.Box3().setFromObject(mesh);
                        box.center(mesh.position); // this re-sets the mesh position
                        mesh.position.y = 0;
                        mesh.position.multiplyScalar(- 1);
                        Shelf.add(mesh);
                    });
                    Shelf.scale.set(0.3, 0.3, 0.3);
                    scene.add(Shelf);
                    worldObjects[command.parameters.guid] = Shelf;
                }
            }
            var object = worldObjects[command.parameters.guid];
            object.position.x = command.parameters.x;
            object.position.y = command.parameters.y;
            object.position.z = command.parameters.z;
            object.rotation.x = command.parameters.rotationX;
            object.rotation.y = command.parameters.rotationY;
            object.rotation.z = command.parameters.rotationZ;
        }
    };
    init();
    animate();
};
