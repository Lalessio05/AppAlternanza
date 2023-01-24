import React, {useEffect, useState} from 'react';
import {View, Text, Button, StyleSheet, TextInput, Alert} from 'react-native';
import io from 'socket.io-client';
import {AsyncStorage} from 'react-native';

function LoginScreen({navigation}) {
  const [username, SetUsername] = useState('');
  const [password, SetPassword] = useState('');
  const [socket, setSocket] = useState(null);
  const [chiave, setChiave] = useState(new ArrayBuffer(512));

  const storeData = async chiave => {
    try {
      await AsyncStorage.setItem('@Key', chiave);
    } catch (e) {
      // saving error
    }
  };
  
  const getData = async () => {
    try {
      const value = await AsyncStorage.getItem('@Key');
      if (value !== null) {
        setChiave(chiave);
      }
    } catch (e) {
      // error reading value
    }
  };
  useEffect(()=>{
    getData()
  })
  if (!socket) {
    setSocket(io('http://192.168.1.239:3000'));
  }

  return (
    <View style={styles.screen}>
      <TextInput
        placeholder="Email"
        style={{borderColor: 'black', borderWidth: 2, width: 100 + '%'}}
        onChangeText={username => {
          SetUsername(username);
        }}
      />
      <TextInput
        placeholder="Password"
        style={{borderColor: 'black', borderWidth: 2, width: 100 + '%'}}
        onChangeText={password => {
          SetPassword(password);
        }}
      />
      <Button
        color={'red'}
        title={'Submit'}
        onPress={() => {
          socket.emit('OnSubmit', {
            username: username,
            password: password,
          });
          socket.on('OnSubmitResponse', messaggio => {
            if (messaggio) {
              setChiave(messaggio);
              navigation.navigate('Main');
            }
          });
        }}
      />
      <Button
        color={'blue'}
        title={'Sono già loggato'}
        onPress={() => {
          socket.emit('OnAutoLogin', chiave);
          socket.on('OnAutoLoginResponse', messaggio => {
            if (messaggio) navigation.navigate('Main');
          });
        }}
      />
      <Button color={"orange"}title={"Save data"}onPress={storeData}/>
      <Button color={"green"}title={"Load data"}onPress={()=>{getData();console.log(chiave)}}/>
    </View>
  );
}

const styles = StyleSheet.create({
  screen: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
});

export default LoginScreen;
