import {useFocusEffect} from '@react-navigation/native';
import React, {useState} from 'react';
import {View, Button, StyleSheet, TextInput, Text, Alert} from 'react-native';
import Socket from '../classi/Socket';

function LoginScreen({navigation}: any) {
  const [username, SetUsername] = useState('');
  const [password, SetPassword] = useState('');
  const [socket, setSocket] = useState<Socket | null>(null);
  useFocusEffect(
    React.useCallback(() => {
      setSocket(new Socket('ws://192.168.1.39:3000'));
      return () => {
        socket?.Disconnetti();
        setSocket(null);
      };
    }, []),
  );
  return (
    <View style={styles.screen}>
      <TextInput
        placeholder="Username"
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
          socket?.Manda('OnSubmit', {username: username, password: password});
          setTimeout(() => {
            if (!risposto) {
              Alert.alert('Il server non risponde');
            }
          }, 5000);
          let risposto = false;
          socket?.Ricevi('OnSubmitResponse', chiave => {
            risposto = true;
            if (chiave) {
              navigation.navigate({
                name: 'Home',
                params: {chiave: chiave, data: new Date().toDateString()},
                merge: true,
              });
              socket.Disconnetti();
            } else Alert.alert('Username e/o password sbagliati');
          });
        }}
      />
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
