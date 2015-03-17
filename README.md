AeroGear OTP Cordova
====================

Cordova plugin for OTP depends on [BarcodeScanner](https://github.com/wildabeast/BarcodeScanner) to be able to easly obtain the secret

|                 | Project Info  |
| --------------- | ------------- |
| License:        | Apache License, Version 2.0  |
| Build:          | Cordova Plugin  |
| Documentation:  | https://aerogear.org/docs/specs/aerogear-cordova/  |
| Issue tracker:  | https://issues.jboss.org/browse/AGCORDOVA  |
| Mailing lists:  | [aerogear-users](http://aerogear-users.1116366.n5.nabble.com/) ([subscribe](https://lists.jboss.org/mailman/listinfo/aerogear-users))  |
|                 | [aerogear-dev](http://aerogear-dev.1069024.n5.nabble.com/) ([subscribe](https://lists.jboss.org/mailman/listinfo/aerogear-dev))  |

## To install

    cordova create <project-name>
    cd <project-name>
    cordova platform add android
    cordova plugin add <location-of-this-plugin>
    cordova build

## iOS
Install [cocapods](http://cocoapods.org/) if you don't have it, run install:

    cd platforms/ios/
    pod install
    open HelloCordova.xcworkspace

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
     generator.generateOTP(function(result) { /* result is the otp */ });

## Documentation

For more details about the current release, please consult [our documentation](https://aerogear.org/docs/specs/aerogear-cordova/).

## Development

If you would like to help develop AeroGear you can join our [developer's mailing list](https://lists.jboss.org/mailman/listinfo/aerogear-dev), join #aerogear on Freenode, or shout at us on Twitter @aerogears.

Also takes some time and skim the [contributor guide](http://aerogear.org/docs/guides/Contributing/)

## Questions?

Join our [user mailing list](https://lists.jboss.org/mailman/listinfo/aerogear-users) for any questions or help! We really hope you enjoy app development with AeroGear!

## Found a bug?

If you found a bug please create a ticket for us on [Jira](https://issues.jboss.org/browse/AGCORDOVA) with some steps to reproduce it.
