/*
 * JBoss, Home of Professional Open Source.
 * Copyright Red Hat, Inc., and individual contributors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WPCordovaClassLib.Cordova.Commands;
using WPCordovaClassLib.Cordova.JSON;

using AeroGear.OTP;
using WPCordovaClassLib.Cordova;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class OtpPlugin : BaseCommand
{
    public void generateOTP(string unparsed)
    {
        var secret = JsonHelper.Deserialize<string[]>(unparsed)[0];

        Totp generator = new Totp(secret);

        PluginResult result = new PluginResult(PluginResult.Status.OK, generator.now());
        DispatchCommandResult(result);
    }

    public async void readSecret(string ignored)
    {
        var token = await new Repository().ReadToken();

        PluginResult result = new PluginResult(PluginResult.Status.OK, token);
        DispatchCommandResult(result);
    }

    public async void storeSecret(string unparsed)
    {
        var secret = JsonHelper.Deserialize<string[]>(unparsed)[0];
        await new Repository().SaveToken(secret);
    }

    private string generate(string secret)
    {
        Totp generator = new Totp(secret);
        return generator.now();
    }

}