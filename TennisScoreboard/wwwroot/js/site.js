const uri = 'api/todoitems';
function addItem() {
    const addNameTextbox = document.getElementById('add-name');

    const item = {
        isComplete: false,
        name: addNameTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}


let firstGamer = document.getElementById('firstGamer');
let valFirstGamer = document.getElementById('firstGamer').value;
let secondGamer = document.getElementById('secondGamer');
let valSecondGamer = document.getElementById('secondGamer').value;
let table = document.getElementById('scoreTable');

function createPost() {
    fetch(`http://localhost:5052/match-score/${valFirstGamer}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    });
}
function createPost2() {
    fetch(`http://localhost:5052/match-score/${valSecondGamer}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    });
}
firstGamer.addEventListener("click", (e) => {
    createPost(Response => {
        console.log(Response);
    });
})
secondGamer.addEventListener("click", (e) => {
    createPost2(Response => {
        console.log(Response);
    });
})
$.ajax