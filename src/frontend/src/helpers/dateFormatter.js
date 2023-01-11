export function formatDate(date) {
    let dateObject = new Date(date);
    let result =
        dateObject.getDate() +
        "/" +
        (dateObject.getMonth() + 1) +
        "/" +
        dateObject.getFullYear() +
        " " +
        dateObject.getHours() +
        ":" +
        dateObject.getMinutes();

    return result;
}
