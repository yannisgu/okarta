angular.module('app.services')
.factory('openLayerService', [function() {
    return {
        config: {'source': {
            'type': 'BingMaps',
            'key': 'ArrGmnldg4Td0C-f2G3mORjdryVUOF3S0P0YGFB_Q6sVl-BMd8Zg20xsGk11Int6',
            'imagerySet': 'Road'
        }}
    }
}]);
