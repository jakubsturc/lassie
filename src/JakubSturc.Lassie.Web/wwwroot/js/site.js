(function (document, window) {

    function onPageLoad() {
        var btn = document.getElementById('btn');
        btn.addEventListener('click', onSearchClicked);
    }

    function onSearchClicked() {
        var code = document.getElementsByName('code')[0].value;
        getJson('search/list', function (sites) {
            showResultSection();
            for (i = 0; i < sites.length; ++i) {
                var site = sites[i];
                createSite(site);
                var searchUrl = 'search' + '/' + site.id + '/' + code;
                getJson(searchUrl, displaySearchResponse);
            }
        });
    }

    function createSite(site) {
        var results = document.getElementById('results');
        var item = document.createElement('li');
        item.setAttribute("id", site.id);
        item.innerHTML = '<a href="' + site.baseUrl + '">' + site.name + '</a> <i class="fas fa-paw fa-spin"></i>';
        results.appendChild(item);
    }

    function displaySearchResponse(response) {
        var item = document.getElementById(response.siteId);
        var elements = item.getElementsByTagName("svg");
        for (i = 0; i < elements.length; ++i) {
            item.removeChild(elements[i]);
        }
        var text = response.worthChecking ? " ✔" : " ❌";
        var node = document.createTextNode(text);
        item.appendChild(node);
    }

    function getJson(url, onload) {
        var req = new XMLHttpRequest();
        req.overrideMimeType('application/json');
        req.open('GET', url, true);
        req.onload = function() {
            var json = JSON.parse(req.responseText);
            onload(json);
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


