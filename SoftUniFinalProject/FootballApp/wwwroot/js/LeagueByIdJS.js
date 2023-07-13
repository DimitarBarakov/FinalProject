let standingsLink = document.querySelector("#showStandings")
let fixturesLink = document.querySelector("#showLeagueFixtures")

let standingsTable = document.querySelector("#standingsTable");
let fixturesTable = document.querySelector("#fixturesTable")

standingsLink.addEventListener('click', showLeagueStandings);
fixturesLink.addEventListener('click', showLeagueFixtures);

function showLeagueStandings() {
    if (standingsTable.style.display == 'none') {
        standingsTable.style.display = 'block';
        fixturesTable.style.display = 'none';
    }

}

function showLeagueFixtures() {
    if (fixturesTable.style.display == 'none') {
        standingsTable.style.display = 'none';
        fixturesTable.style.display = 'block';
    }

}