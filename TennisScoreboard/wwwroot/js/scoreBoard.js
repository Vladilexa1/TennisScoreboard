const uri = 'http://localhost:5052';
let firstGamer = document.getElementById('firstGamer');
let secondGamer = document.getElementById('secondGamer');

let table = document.getElementById('scoreTable');
let currentUrl = window.location.href;
let urlParams = new URLSearchParams(new URL(currentUrl).search);
let guid = urlParams.get('guid');



function addPoint(buttonValue) {
    const params = new URLSearchParams();
    params.append('id', buttonValue);

    fetch(`${uri}/match-score?guid=${guid}`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: params.toString()
    })
        .then(response => response.json())
        .then(data => _displayItems(data))
}
function _displayItems(data) {
    const tBody = document.getElementById('table-body');
    tBody.innerHTML = '';

    const items = Object.values(data);

    if (data.winnerName == undefined) {
        items.forEach(item => {
            let editButton;

            if (item === items[0]) {
                editButton = firstGamer.cloneNode(true);
            } else {
                editButton = secondGamer.cloneNode(true);
            }

            let tr = tBody.insertRow();

            let td1 = tr.insertCell(0);
            let name = document.createTextNode(item.name);
            td1.appendChild(name);

            let td2 = tr.insertCell(1);
            let set = document.createTextNode(item.set);
            td2.appendChild(set);

            let td3 = tr.insertCell(2);
            let game = document.createTextNode(item.game);
            td3.appendChild(game);

            let td4 = tr.insertCell(3);
            let textNode = document.createTextNode(item.point);
            td4.appendChild(textNode);

            let td5 = tr.insertCell(4);
            td5.appendChild(editButton);

            editButton.addEventListener("click", (e) => {
                const buttonValue = e.target.getAttribute('data-id');
                addPoint(buttonValue, Response => {
                    console.log(Response);
                });
            })
        });
    } else {
        let container = document.getElementById('window-score');

        container.innerHTML = '';
        let newA = document.createElement('a');
        newA.textContent = `Winner: ${data.winnerName}`;
        newA.className = 'button-point'
        newA.href = `${uri}`
        container.appendChild(newA);
        
    }
}

firstGamer.addEventListener("click", (e) => {
    const buttonValue = e.target.getAttribute('data-id');
    addPoint(buttonValue, Response => {
        console.log(Response);
    });
})
secondGamer.addEventListener("click", (e) => {
    const buttonValue = e.target.getAttribute('data-id');
    addPoint(buttonValue, Response => {
        console.log(Response);
    });
})
