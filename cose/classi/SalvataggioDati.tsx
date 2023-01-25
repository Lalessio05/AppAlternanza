import AsyncStorage from '@react-native-async-storage/async-storage';
export default class {
  static storeData = async (item: any) => {
    const jsonValue = JSON.stringify(item);
    await AsyncStorage.setItem('@Key', jsonValue);
  };
  static getData = async () => {
    const jsonValue = await AsyncStorage.getItem('@Key');
    return jsonValue != null ? JSON.parse(jsonValue) : null;
  };
}
