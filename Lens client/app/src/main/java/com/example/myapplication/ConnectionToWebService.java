package com.example.myapplication;

import android.content.Context;
import android.graphics.Bitmap;
import android.util.Base64;
import android.util.Log;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;

import java.io.ByteArrayOutputStream;
import java.io.Console;
import java.util.HashMap;
import java.util.Map;

public class ConnectionToWebService {

    private static ConnectionToWebService connectionToWebService = new ConnectionToWebService();
    private final String URL = "http://135.128.14.150/Hackathon_2019/api/categories";
    private  final RequestQueue queue = null;
    public static Bitmap pictureHasTaken;
    private  ConnectionToWebService(){


    };


    public static ConnectionToWebService getConnectionToWebService(){
        if(connectionToWebService == null)
            connectionToWebService = new ConnectionToWebService();
        return connectionToWebService;
    }

    public StringRequest SendDataToServer(final String data) {

        StringRequest request = new StringRequest(Request.Method.GET, URL, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                Log.d("Response:  ", response);
                JSONArray jsonArray = null;
                try {
                    jsonArray = new JSONArray(response);
                } catch (JSONException e) {
                    e.printStackTrace();
                }
                //    JSONObject json = new JSONObject(response);
                //    String xml = XML.toString(json);

            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Log.d("error",error.toString());
            }
        })
        {
            @Override
            protected Map<String, String> getParams() throws AuthFailureError {
                Map<String, String> params = new HashMap<>();
                params.put("imageTaken", data);
                return params;
            }
        };

        return request;
    }

 /*   private String imageToString(Bitmap pictureHasTaken) {
        ByteArrayOutputStream baos=new  ByteArrayOutputStream();
        pictureHasTaken.compress(Bitmap.CompressFormat.PNG,100, baos);
        byte [] b=baos.toByteArray();
        String temp=Base64.encodeToString(b, Base64.DEFAULT);
        return temp;
    }*/


}
