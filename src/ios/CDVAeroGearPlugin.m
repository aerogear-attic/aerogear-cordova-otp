//
//  CDVAeroGearPlugin.m
//  HelloCordova
//
//  Created by Erik Jan de Wit on 20/9/13.
//
//

#import "CDVAeroGearPlugin.h"

@implementation CDVAeroGearPlugin

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
