

'use strict';

angular.module('app.auth').factory('User', function ($http, $q, APP_CONFIG) {
    var dfd = $q.defer();

    var UserModel = {
        initialized: dfd.promise,
        username: undefined,
        picture: undefined
    };
     $http.get(APP_CONFIG.apiRootUrl + '/user.json').then(function(response){
         UserModel.username = response.data.username;
         UserModel.picture= response.data.picture;
         dfd.resolve(UserModel)
     });

    return UserModel;
});
