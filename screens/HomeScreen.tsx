import React, {useEffect, useState} from 'react';
import Socket from '../classi/Socket';
import {Alert, Button, Text, View} from 'react-native';
import SalvataggioDati from '../classi/SalvataggioDati';
import {useFocusEffect} from '@react-navigation/native';

export default function HomeScreen({navigation, route}: any) {
  const [chiave, setChiave] = useState('');
  const [socket, setSocket] = useState<Socket | null>(null);
  const [lastTockenDate, setLastTokenDate] = useState<string>('');
  async function fetchData() {
    setChiave(await SalvataggioDati.getData('@Key'));
    setLastTokenDate(await SalvataggioDati.getData('@Date'));
  }

  useFocusEffect(
    React.useCallback(() => {
      setSocket(new Socket('ws://192.168.1.39:4500'));
      return () => {
        socket?.Disconnetti();
        setSocket(null);
      };
    }, []),
  );

  useFocusEffect(
    React.useCallback(() => {
      if (route.params?.chiave !== null) {
        setChiave(route.params?.chiave);
        setLastTokenDate(route.params?.data);
      }
    }, [route.params?.chiave, route.params?.data]),
  );

  useEffect(() => {}, [socket]);

  useEffect(() => {
    if (chiave !== '' && chiave !== null)
      SalvataggioDati.storeData('@Key', chiave);
  }, [chiave]);

  useEffect(() => {
    if (lastTockenDate !== '' && lastTockenDate !== null)
      SalvataggioDati.storeData('@Date', lastTockenDate);
  }, [lastTockenDate]);

  useEffect(() => {
    if (chiave === '') {
      fetchData();
    }
  }, []);
  useEffect(() => {}, [socket]);

  return (
    <View>
      <Button
        color={'red'}
        title="Ho bisogno di un token"
        onPress={() => navigation.navigate('Log-in')}
      />
      <Button
        color={'blue'}
        title="Credo di avere già un token"
        onPress={() => {
          socket?.Manda('OnStart', {username: 'Gianni'});
          socket?.Manda('OnAutoLogin', {codice: chiave});
          let risposto = false;
          setTimeout(() => {
            if (!risposto) {
              Alert.alert('Il server non risponde');
            }
          }, 5000);
          socket?.Ricevi('OnAutoLoginResponse', risposta => {
            risposto = true;
            if (risposta) {
              navigation.navigate({
                name: 'Main',
                params: {chiave: chiave},
                merge: true,
              });
            } else Alert.alert('Il token non è valido');
          });
        }}
      />
      <Button title="Prova" onPress={() => console.log(chiave)} />
      <Text>Ultimo token ricevuto: {lastTockenDate}</Text>
    </View>
  );
}
