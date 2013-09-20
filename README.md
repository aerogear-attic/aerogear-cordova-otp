AeroGear OTP Cordova
====================

Cordova plugin for OTP comes with [BarcodeScanner](/wildabeast/BarcodeScanner) to be able to easly obtain the secret

## To install

    cordova create <project-name>
    cd <project-name>
    cordova platform add android
    cordova plugin add <location-of-this-plugin>
    cordova build

## Example
Copy ``example/*`` into the ``www`` folder of your project and press the OTP button. Initially it will fire up the 
BarcodeScanner to scan a QR code with an url like ``otpauth://totp/username?secret=7SPQJZ7CDF7NTKJ2`` this secret will be 
stored and then later used to generate One Time Passwords have a look at the [guide](http://aerogear.org/docs/guides/AeroGear-OTP/)

Try it on this [demo page](http://controller-aerogear.rhcloud.com/aerogear-controller-demo/login) username is ``john`` and password ``123``

## Plugin API

	var totp = new AeroGear.Totp();
    totp.generate(function(result) { /* result is the otp */ );

or seperate methods

     String secret = "B2374TNIQ3HKC446";
     // initialize OTP
     var generator = new AeroGear.Totp(secret);
     // generate token
     generator.generateOTP(function(result) { /* resutl is the otp */ });

