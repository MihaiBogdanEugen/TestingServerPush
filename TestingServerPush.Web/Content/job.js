function initMap() {

    var mapDiv = window.detailsArgs.map;
    var positionsAsJson = window.detailsArgs.positionsAsJson;
    var startPosition = window.detailsArgs.startPosition;
    var endPosition = window.detailsArgs.endPosition;

    initMapOneWay(mapDiv, positionsAsJson, startPosition, endPosition);
}

function middle(startPosition, endPosition) {
    var middleLat = (startPosition.lat + endPosition.lat) / 2;
    var middleLng = (startPosition.lng + endPosition.lng) / 2;

    return {
        lat: middleLat,
        lng: middleLng,
    }
}

function initMapOneWay(mapDiv, points, startPosition, endPosition) {

    googleMap = new google.maps.Map(document.getElementById(mapDiv), {
        center: middle(startPosition, endPosition),
        zoom: 12,
        panControl: true,
        zoomControl: true,
        mapTypeControl: false,
        scaleControl: true,
        streetViewControl: false,
        overviewMapControl: true,
        rotateControl: true,
        scrollwheel: true,
        disableDoubleClickZoom: false,
        keyboardShortcuts: true
    });

    new google.maps.Marker({
        position: startPosition,
        map: googleMap,
        title: 'START'
    });

    new google.maps.Marker({
        position: endPosition,
        map: googleMap,
        title: 'FINISH'
    });

    if (flightPlan) {
        flightPlan.setMap(null);
    }

    if (markers.length !== 0) {
        for (var i2 = 0; i2 < markers.length; i2++) {
            markers[i2].setMap(null);
        }
    }
    markers.length = 0;

    for (var i = 0; i < points.length; i++) {
        markers.push(new google.maps.Marker({
            position: points[i],
            map: googleMap,
            icon: {
                url: 'http://maps.google.com/mapfiles/kml/shapes/cycling.png',
                scaledSize: new google.maps.Size(16, 16),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(8, 8)
            }
        }));
    }

    flightPlan = new google.maps.Polyline({
        path: points,
        geodesic: true,
        strokeColor: '#FF0000',
        strokeOpacity: 1.0,
        strokeWeight: 3
    });
    flightPlan.setMap(googleMap);
}