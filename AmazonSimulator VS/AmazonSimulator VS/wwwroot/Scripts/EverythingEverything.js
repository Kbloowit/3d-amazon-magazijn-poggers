function parseCommand(input = "") {
    return JSON.parse(input);
}

var exampleSocket;

/** Check when webpage is loaded */
window.onload = function () {
    var camera, scene, renderer;
    var cameraControls;

    var worldObjects = {};
    /** Occurs upon loading the page, initializes the world */
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
        var material = new THREE.MeshPhongMaterial({ map: new THREE.TextureLoader().load("textures/floor/floor.jpg"), side: THREE.DoubleSide });
        var plane = new THREE.Mesh(geometryPlane, material);
        plane.rotation.x = Math.PI / 2.0;
        plane.position.y = 0;
        plane.position.x = 15;
        plane.position.z = 15;
        scene.add(plane);

        var worldStandards = new WorldStandards();
        scene.add(worldStandards);

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

    /** Adjusts the window size on window size change */
    function onWindowResize() {
        camera.aspect = window.innerWidth / window.innerHeight;
        camera.updateProjectionMatrix();
        renderer.setSize(window.innerWidth, window.innerHeight);
    }

    /** Can be used to reqeust animations */
    function animate() {
        requestAnimationFrame(animate);
        cameraControls.update();
        renderer.render(scene, camera);
    }

    // Creates socket connection
    exampleSocket = new WebSocket('ws://' + window.location.hostname + ':' + window.location.port + '/connect_client');
    /**
     * Does things upon recieving commands, like loading objects
     * @param {any} event command
     */
    exampleSocket.onmessage = function (event) {
        var command = parseCommand(event.data);
        if (command.command === 'update') {
            if (Object.keys(worldObjects).indexOf(command.parameters.guid) < 0) {
                if (command.parameters.type === 'truck') {
                   Truck = new Truck();
                    scene.add(Truck);
                    worldObjects[command.parameters.guid] = Truck;
                }
                if (command.parameters.type === 'robot') {
                    var robot = new Robot();

                    scene.add(robot);
                    worldObjects[command.parameters.guid] = robot;
                }
                if (command.parameters.type === 'shelf') {
                    var shelf = new Shelf();
                    scene.add(shelf);
                    worldObjects[command.parameters.guid] = shelf;

                }
                if (command.parameters.type === 'train') {
                    var train = new Train();
                    scene.add(train);
                    worldObjects[command.parameters.guid] = train;

                }
                if (command.parameters.type === 'forklift') {
                    var forklift = new Forklift();

                    scene.add(forklift);
                    worldObjects[command.parameters.guid] = forklift;
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