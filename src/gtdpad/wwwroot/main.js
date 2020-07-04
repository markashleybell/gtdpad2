const endpointSelector = document.getElementById('endpoints');
const form = document.getElementById('harness');
const idField = form.querySelector('[name=ID]');
function makeRequest() {
    const [method, endpoint] = endpointSelector.value.split(' ');
    const formData = new FormData(form);
    const data = Array.from(formData).reduce((o, [k, v]) => (o[k] = v, o), {});
    const payload = JSON.stringify(data);
    const url = endpoint.replace('{id}', idField.value);
    const request = new Request(url, {
        method: method,
        body: payload,
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });
    fetch(request)
        .then(res => res.json())
        .then(res => console.log(res))
        .catch(err => console.log(err));
}
form.addEventListener('submit', function (e) {
    e.preventDefault();
    makeRequest();
});
//# sourceMappingURL=main.js.map