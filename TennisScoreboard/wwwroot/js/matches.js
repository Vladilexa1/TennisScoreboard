const uri = 'http://localhost:5052';

let playerName = '';

let searchButton = document.getElementById('searchButton');

let currentUrl = window.location.href;
let urlParams = new URLSearchParams(new URL(currentUrl).search);
let pageValue = parseInt(urlParams.get('page'));
let previousPageTd = document.getElementById('previousPageTd');
let nextPageTd = document.getElementById('nextPageTd');
nextButtonWriter();
previousButtonWriter();
let nextPage = document.getElementById('nextPage');
nextPage.addEventListener("click", (e) => {
    pageValue++;
    openPage()
        .then(response => {
            console.log(response);
        });
});
searchButton.addEventListener("click", (e) => {
    let inputName = document.getElementById('inputName');
    playerName = inputName.value;
    pageValue = 1;
    openPage()
        .then(response => {
            console.log(response);
        });
});
function nextButtonWriter() {
    if (hasDataInTable()) { // если таблица меньше 7, кнопку не рисуем
        nextPageTd.innerHTML = ''
        nextPageTd.className = ''
        let nextPage = document.createElement('button');
        nextPage.className = 'button-nextPage';
        nextPage.id = 'nextPage';
        nextPage.textContent = '>';
        nextPageTd.appendChild(nextPage);
        return;
    }
    fetch(`${uri}/matches?playerName=${playerName}&page=${pageValue + 1}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/html'
        }
    }).then(Response => {
        if (Response.status != 404) {
            nextPageTd.innerHTML = ''
            nextPageTd.className = ''
            let nextPage = document.createElement('button');
            nextPage.className = 'button-nextPage';
            nextPage.id = 'nextPage';
            nextPage.textContent = '>';
            nextPageTd.appendChild(nextPage);
        }
    });
}
function previousButtonWriter() {
    if (pageValue != 1) {
        previousPageTd.innerHTML = ''
        previousPageTd.className = ''
        let previousPage = document.createElement('button');
        previousPage.className = 'button-nextPage';
        previousPage.id = 'previousPage';
        previousPage.textContent = '<';
        previousPageTd.appendChild(previousPage);
        previousPage.addEventListener("click", (e) => {
            pageValue--;
            openPage()
                .then(response => {
                    console.log(response);
                });
        });
    }
}
function hasDataInTable() {
    let table = document.getElementById('table');
    return table.rows.length == 7; // 7 i`ts header + 5 row data + row button
}
function openPage() {

    return fetch(`${uri}/matches?playerName=${playerName}&page=${pageValue}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/html'
        }
    })
        .then(response => {
            response.json()
            window.location.href = `${uri}/matches?playerName=${playerName}&page=${pageValue}`
            playerName = ''
        });
}