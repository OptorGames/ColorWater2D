var DevToDev = {
    $dtd: {},
    initialize: function (appKey, deviceId) {
        window.devtodev.initialize(Pointer_stringify(appKey), Pointer_stringify(deviceId));
    },
    getSdkVersion: function () {
        var result = window.devtodev.sdkVersion;
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    getAppVersion: function () {
        var result = window.devtodev.appVersion;
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    setAppVersion: function (version) {
        window.devtodev.appVersion = Pointer_stringify(version);
    },
    getUserId: function () {
        var result = window.devtodev.userId;
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    setUserId: function (userId) {
        window.devtodev.appVersion = Pointer_stringify(userId);
    },
    getCurrentLevel: function () {
        var result = window.devtodev.currentLevel;
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    setCurrentLevel: function (level) {
        window.devtodev.currentLevel = level;
    },
    getLogLevel: function () {
        var result = window.devtodev.logLevel;
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    setLogLevel: function (logLevel) {
        window.devtodev.logLevel = Pointer_stringify(logLevel);
    },
    getTrackingAvailability: function () {
        var result = window.devtodev.trackingAvailability;
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    setTrackingAvailability: function (value) {
        window.devtodev.trackingAvailability = value;
    },
    referrer: function (value) {
        var utm = Pointer_stringify(value);
        var arr = utm.split('&');
        var utm_data = [];
        for (var i = 0; i < arr.length; i++) {
            var t = arr[i].split('=');
            utm_data.push(t[0]);
            utm_data.push(t[1]);
        }
        window.devtodev.referrer(utm_data);
    },
    virtualCurrencyPayment: function (json) {
        try {
            var data = JSON.parse(Pointer_stringify(json));
            var purchaseId =data.purchaseId;
            var purchaseType =data.purchaseType;
            var purchaseAmount =data.purchaseAmount;
            var purchasePrice =data.purchasePrice;
            var purchaseCurrency =data.purchaseCurrency;
            var args ={};
            args[purchaseCurrency] = purchasePrice;
            window.devtodev.virtualCurrencyPayment(purchaseId, purchaseType, purchaseAmount, args);
        } catch (e) {
            console.log(e);
        }
    },
    currencyAccrual: function (json) {
        var data = JSON.parse(Pointer_stringify(json));
        var currencyName =data.currencyName;
        var currencyAmount =data.currencyAmount;
        var currencySource =data.currencySource;
        var accrualType =data.accrualType;
        window.devtodev.currencyAccrual(currencyName, currencyAmount,currencySource,accrualType);
    },
    realCurrencyPayment: function (orderId, price, productId, currencyCode) {
        window.devtodev.realCurrencyPayment(Pointer_stringify(orderId), price, Pointer_stringify(productId), Pointer_stringify(currencyCode));
    },
    tutorial: function (step) {
        window.devtodev.tutorial(step);
    },
    socialNetworkConnect: function (name) {
        window.devtodev.socialNetworkConnect(Pointer_stringify(name));
    },
    socialNetworkPost: function (name, reason) {
        window.devtodev.socialNetworkPost(Pointer_stringify(name), Pointer_stringify(reason));
    },
    sendBufferedEvents: function () {
        window.devtodev.sendBufferedEvents();
    },
    startProgressionEvent: function (val, valJSON) {
        var json = JSON.parse(Pointer_stringify(valJSON));
        window.devtodev.startProgressionEvent(Pointer_stringify(val), json);
    },
    finishProgressionEvent: function (val, valJSON) {
        var json = JSON.parse(Pointer_stringify(valJSON));
        window.devtodev.finishProgressionEvent(Pointer_stringify(val),json);
    },
    customEvent: function (name, params) {
        window.devtodev.customEvent(Pointer_stringify(name), params)
    },
    replaceUserId: function (previousUserId, userId) {
        window.devtodev.replaceUserId(Pointer_stringify(previousUserId), Pointer_stringify(userId));
    },
    levelUp: function (level, jsonString) {
        var convertedJson = Pointer_stringify(jsonString);
        if (convertedJson.length == 0) {
            window.devtodev.levelUp(level, null, null, null, null);
        } else {
            var balance = window.createDictionaryFromJson(convertedJson, 'balance');
            var spent = window.createDictionaryFromJson(convertedJson, 'spent');
            var earned = window.createDictionaryFromJson(convertedJson, 'earned');
            var bought = window.createDictionaryFromJson(convertedJson, 'bought');
            window.devtodev.levelUp(level, balance, spent, earned, bought)
        }
    },
    userName: function (value) {
        window.devtodev.user.name = Pointer_stringify(value);
    },
    getUserName: function () {
        var result = window.devtodev.user.name;
        if (result == null) result = '';
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    userAge: function (value) {
        window.devtodev.user.age = value;
    },
    getUserAge: function () {
        return window.devtodev.user.age;
    },
    userCheater: function (value) {
        var result = value == 1;
        window.devtodev.user.cheater = result;
    },
    getUserCheater: function () {
        return window.devtodev.user.cheater;
    },
    userGender: function (value) {
        window.devtodev.user.gender = value;
    },
    getUserGender: function () {
        return window.devtodev.user.gender;
    },
    userEmail: function (value) {
        window.devtodev.user.email = Pointer_stringify(value);
    },
    getUserEmail: function () {
        var result = window.devtodev.user.email;
        if (result == null) result = '';
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    userPhone: function (value) {
        window.devtodev.user.phone = Pointer_stringify(value);
    },
    getUserPhone: function () {
        var result = window.devtodev.user.phone;
        if (result == null) result = '';
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    userPhoto: function (value) {
        window.devtodev.user.photo = Pointer_stringify(value);
    },
    getUserPhoto: function () {
        var result = window.devtodev.user.photo;
        if (result == null) result = '';
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    userUnset: function (jsonKeys) {
        var data = JSON.parse(Pointer_stringify(jsonKeys));
        var keys = data.keys;
        window.devtodev.user.unset(keys);
    },
    userDelete: function () {
        window.devtodev.user.deleteUser();
    },
    userSet: function (json) {
        var data = JSON.parse(Pointer_stringify(json));
        var key = data.key;
        var value = data.value;
        window.devtodev.user.set(key, value);
    },
    userSetOnce: function (json) {
        var data = JSON.parse(Pointer_stringify(json));
        var key = data.key;
        var value = data.value;
        window.devtodev.user.setOnce(key, value);
    },
    userGet: function (key) {
        var value = window.devtodev.user.getValues(Pointer_stringify(key));
        if (value == null) return null;
        var result = '{"type":';
        result += typeof (value);
        result += ',value:' + value + '}';
        var buffer = _malloc(lengthBytesUTF8(result) + 1);
        var bufferSize = lengthBytesUTF8(result) + 1;
        stringToUTF8(result, buffer, bufferSize);
        return buffer;
    },
    executeRemoteConfig: function (config) {
        window.devtodev.executeRemoteConfig(Pointer_stringify(config));
    }
};
autoAddDeps(DevToDev, '$dtd');
mergeInto(LibraryManager.library, DevToDev);
