package org.jboss.aerogear.cordova.android;

import android.content.SharedPreferences;
import android.util.Log;
import org.apache.cordova.CallbackContext;
import org.apache.cordova.CordovaInterface;
import org.apache.cordova.CordovaPlugin;
import org.apache.cordova.CordovaWebView;
import org.jboss.aerogear.security.otp.Totp;
import org.json.JSONArray;
import org.json.JSONException;

import static android.content.SharedPreferences.Editor;

/**
 * @author edewit@redhat.com
 */
public class AeroGearPlugin extends CordovaPlugin {
  public static final String TAG = AeroGearPlugin.class.getSimpleName();

  private static final String OTP = "generateOTP";
  private static final String STORE = "storeSecret";
  private static final String READ = "readSecret";

  public static final String PARAM_SECRET = "secret";

  private SharedPreferences preferences;

  @Override
  public void initialize(CordovaInterface cordova, CordovaWebView webView) {
    this.cordova = cordova;
    preferences = cordova.getActivity().getApplicationContext().getSharedPreferences(TAG, 0);
  }

  @Override
  public boolean execute(final String action, final JSONArray data, final CallbackContext callbackContext) {
    Log.v(TAG, "execute action: " + action);
    try {

      if (OTP.equals(action)) {
        final String secret = data.getString(0);
        cordova.getThreadPool().execute(new Runnable() {
          public void run() {
            callbackContext.success(generateOTP(secret));
          }
        });
        return true;
      }

      if (STORE.equals(action)) {
        String secret = data.getString(0);
        storeSecret(secret);
        return true;
      }

      if (READ.equals(action)) {
        final String secret = preferences.getString(PARAM_SECRET, null);
        callbackContext.success(secret);
        return true;
      }

    } catch (JSONException e) {
      callbackContext.error("execute: Got JSON Exception " + e.getMessage());
    }

    return false;
  }

  private String generateOTP(String secret) {
    Totp generator = new Totp(secret);
    return generator.now();
  }

  private void storeSecret(String secret) {
    final Editor editor = preferences.edit();
    editor.putString(PARAM_SECRET, secret);
    editor.commit();
  }
}
