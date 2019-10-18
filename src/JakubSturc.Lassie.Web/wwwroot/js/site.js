(function (document, window) {

    function onPageLoad() {
        var btn = document.getElementById('btn');
        btn.addEventListener('click', onSearchClicked);
    }

    function onSearchClicked() {

        showResultSection();

        var code = document.getElementsByName('code')[0].value;
        getJson('search/list', function (sites) {
            for (i = 0; i < sites.length; ++i) {
                var site = sites[i];
                var searchUrl = 'search' + '/' + site + '/' + code;
                getJson(searchUrl, displaySearchResponse);
            }
        });
    }

    function displaySearchResponse(res) {
        displayDebug(res);
        var results = document.getElementById('results');
        var item = document.createElement('li');
        item.innerHTML = '<a href="' + res.url + '">' + res.siteId + '</a> ' + (res.worthChecking ? '✔' : '❌');
        results.appendChild(item);
    }

    function getJson(url, onload) {
        var req = new XMLHttpRequest();
        req.overrideMimeType('application/json');
        req.open('GET', url, true);
        req.onload = function () {
            var json = JSON.parse(req.responseText);
            onload(json)
        };
        req.send(null);
    }

    function displayDebug(obj) {
        console.debug(JSON.stringify(obj));
    }

    function showResultSection() {
        // clear reusults
        var results = document.getElementById('results');
        results.innerHTML = '';

        // unhide what is hidden
        var elements = document.getElementsByClassName('is-hidden');
        for (i = 0; i < elements.length; ++i) {
            elements[i].classList.remove('is-hidden')
        }
    }

    window.addEventListener('load', onPageLoad, false);

}(document, window))


