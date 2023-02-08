import AsyncStorage from '@react-native-async-storage/async-storage';
export default class {
  static storeData = async (chiave: string, item: any) => {
    const jsonValue = JSON.stringify(item);
    await AsyncStorage.setItem(chiave, jsonValue, () =>
      console.log('Ho finito di storare il dato ' + item),
    );
  };
  static getData = async (chiave: string) => {
    const jsonValue = await AsyncStorage.getItem(chiave, () =>
      console.log('Finito di recuperare'),
    );
    return jsonValue != null ? JSON.parse(jsonValue) : null;
  };
}
