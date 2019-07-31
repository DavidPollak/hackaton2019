package com.example.myapplication;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.CheckBox;


import java.util.ArrayList;
import java.util.List;

public class UserConfigurationActivity extends Activity{


  //  private List<CheckBox> checkBoxes = new ArrayList<>();
    private List<CheckBox> checkBoxeschecked = new ArrayList<>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.user_configuration);
     //   initializeChecksBoxes();
    }

    public List<CheckBox> getAllCheckedCheckBox(){
        return checkBoxeschecked;
    }
 /*   private void initializeChecksBoxes() {
        CheckBox check1 = (CheckBox)findViewById(R.id.kosher);
        CheckBox check2 = (CheckBox)findViewById(R.id.calories);
        CheckBox check3 = (CheckBox)findViewById(R.id.lactose);
        CheckBox check4 = (CheckBox)findViewById(R.id.sugar_free);
        CheckBox check5 = (CheckBox)findViewById(R.id.vegan);
        CheckBox check6 = (CheckBox)findViewById(R.id.meat);
        checkBoxes.add(check1);
        checkBoxes.add(check2);
        checkBoxes.add(check3);
        checkBoxes.add(check4);
        checkBoxes.add(check5);
        checkBoxes.add(check6);

    }*/


    public void onCheckboxClicked(View view) {
        CheckBox checkBox = (CheckBox)view;
        if(!checkBoxeschecked.contains(checkBox))
            checkBoxeschecked.add(checkBox);
        else
            checkBoxeschecked.remove(checkBox);
    }

    public  List<CheckBox> onSubmitPreference() {
        this.finish();
        return checkBoxeschecked;
    }
}
