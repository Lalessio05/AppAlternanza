import React, {useState} from 'react';
import {View, Text, Button, StyleSheet, TextInput} from 'react-native';

export default function MainScreen(){
    return(
        <View style={{flex:1,justifyContent:'center',alignItems:"center"}}>
            <Text>
                Benvenuto alla main screen, daje roma!
            </Text>
        </View>
    )
}