function addTableRewards(reward, date) {
    return '<tr>'
        + `<td>${reward.type}</td>`
        + `<td class='posXP'>${reward.expierence}</td>`
        + `<td>DESC!!!</td>`
        + `<td class='timeData'>${date.getUTCHours() + ':' + date.getMinutes() + ':' + date.getUTCSeconds()}</td>`
        + '</tr>'
}

function createRewardsHeader() {
    return "<thead> <tr>"
        + "<th scope='col'>Achievment</th>"
        + "<th scope='col'>XP</th>"
        + "<th scope='col'>Description</th>"
        + "<th scope='col'>Time</th>"
        + "</tr> </thead>"
        + "<tbody> </tbody>";
}