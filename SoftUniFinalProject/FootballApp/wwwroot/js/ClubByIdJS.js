let playersLink = document.querySelector("#showPlayers")
let fixturesLink = document.querySelector("#showFixtures")

let playersTable = document.querySelector("#clubPlayers");
let fixturesTable = document.querySelector("#clubFixtures")

playersLink.addEventListener('click', showClubPlayers);
fixturesLink.addEventListener('click', showClubFixtures);

function showClubPlayers() {
    if (playersTable.style.display == 'none') {
        playersTable.style.display = 'block';
        fixturesTable.style.display = 'none';
    }

}

function showClubFixtures() {
    if (fixturesTable.style.display == 'none') {
        playersTable.style.display = 'none';
        fixturesTable.style.display = 'block';
    }

}