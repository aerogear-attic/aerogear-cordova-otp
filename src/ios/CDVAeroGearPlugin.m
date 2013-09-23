//
//  CDVAeroGearPlugin.m
//  HelloCordova
//
//  Created by Erik Jan de Wit on 20/9/13.
//
//

#import "CDVAeroGearPlugin.h"

@implementation CDVAeroGearPlugin

- (void)generateOTP:(CDVInvokedUrlCommand*)command
{
    [self.commandDelegate runInBackground:^{
        NSString *secret = [command.arguments objectAtIndex:0];

        AGTotp *generator = [[AGTotp alloc] initWithSecret:[AGBase32 base32Decode:secret]];
        NSString *totp = [generator generateOTP];

        CDVPluginResult* pluginResult = [CDVPluginResult resultWithStatus:CDVCommandStatus_OK messageAsString:totp];
        [self.commandDelegate sendPluginResult:pluginResult callbackId:command.callbackId];
    }];
}

- (void)readSecret:(CDVInvokedUrlCommand*)command
{
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    NSString *secret = [defaults objectForKey:@"secret"];
    
    CDVPluginResult* pluginResult = [CDVPluginResult resultWithStatus:CDVCommandStatus_OK messageAsString:secret];
    [self.commandDelegate sendPluginResult:pluginResult callbackId:command.callbackId];
}

- (void)storeSecret:(CDVInvokedUrlCommand*)command
{
    NSString *secret = [command.arguments objectAtIndex:0];
    NSUserDefaults *defaults = [NSUserDefaults standardUserDefaults];
    [defaults setObject:secret forKey:@"secret"];
    [defaults synchronize];
    
    CDVPluginResult* pluginResult = [CDVPluginResult resultWithStatus:CDVCommandStatus_OK];
    [self.commandDelegate sendPluginResult:pluginResult callbackId:command.callbackId];
}


- (void)generate:(CDVInvokedUrlCommand*)command
{
    CDVPluginResult* pluginResult = nil;
    NSString* myarg = [command.arguments objectAtIndex:0];
    
    if (myarg != nil) {
        pluginResult = [CDVPluginResult resultWithStatus:CDVCommandStatus_OK];
    } else {
        pluginResult = [CDVPluginResult resultWithStatus:CDVCommandStatus_ERROR messageAsString:@"Arg was null"];
    }
    [self.commandDelegate sendPluginResult:pluginResult callbackId:command.callbackId];
}

@end
