package com.example.myapplication;
import android.content.res.AssetManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.util.Base64;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import com.android.volley.RequestQueue;
import com.android.volley.toolbox.Volley;
import java.io.ByteArrayOutputStream;

public class MainActivity extends AppCompatActivity {

    private RegisterNewUser registerUserActivity;
    private UserConfigurationActivity userConfigurationActivity;
    private String[] userPreferencesChoosen;
    private final int CAMERA_REQUEST = 1;
    private TextView  myChosenProduct;
    private  ImageView imageView;
    RequestQueue queue;
    private final String URL = "http://135.128.14.150/Hackathon_2019/api/categories";



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Button turnOnCameraForRecognizeProducts = (Button)findViewById(R.id.takePicture);
        turnOnCameraForRecognizeProducts.setOnClickListener((new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intentCamera = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
                startActivityForResult(intentCamera,CAMERA_REQUEST);
            }
        }));

        imageView = (ImageView)findViewById(R.id.result_image);
        imageView.setImageBitmap(null);
        myChosenProduct = (TextView)findViewById(R.id.myChosenProduct);
        myChosenProduct.setVisibility(TextView.INVISIBLE);
    }


    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        //if picture was taken properly
        if(requestCode==CAMERA_REQUEST && resultCode==RESULT_OK) {
            myChosenProduct.setVisibility(TextView.VISIBLE);
            //My picture located under the key "data" in intent
            Intent pickPhoto = new Intent(Intent.ACTION_PICK,
                    android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI);
            Bitmap bitmap = (Bitmap)data.getExtras().get("data");
            imageView.setImageBitmap(bitmap);
            queue = Volley.newRequestQueue(this);
            queue.add(ConnectionToWebService.getConnectionToWebService().SendDataToServer(imageToString(bitmap)));
            //   sendImageToServer(bitmap);
        }
    }

    private String imageToString(Bitmap pictureHasTaken) {
        ByteArrayOutputStream baos=new  ByteArrayOutputStream();
        pictureHasTaken.compress(Bitmap.CompressFormat.PNG,100, baos);
        byte [] b=baos.toByteArray();
        String temp= Base64.encodeToString(b, Base64.DEFAULT);
        return temp;
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {

        //creating the connection between the xml menu to java
        getMenuInflater().inflate(R.menu.main_menu,menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        Intent userChoosenIntent;
        //checking the id was pressed on the menu
        switch(item.getItemId()) {

            case R.id.user_configuration:
             //   if(registerUserActivity != null){
                    //load the user Config intent from the class loader
                    userChoosenIntent = new Intent(MainActivity.this, UserConfigurationActivity.class);
                    startActivity(userChoosenIntent);
           //     }
            //    else
            //        Toast.makeText(MainActivity.this,"You Must Register First",Toast.LENGTH_LONG);

                break;
            case R.id.user_register:
//                //load the user Config intent from the class loader
                userChoosenIntent = new Intent(MainActivity.this, RegisterNewUser.class);
                startActivity(userChoosenIntent);

                break;

            default:

        }
        return super.onOptionsItemSelected(item);
    }

}
