export interface Device {
  guid?: string; //only response, not request

  //overview[] response
  name: string;
  deviceTypeId: string;
  failsafe: boolean;

  //detailed response
  id: string;
  tempMin: number;
  tempMax: number;
  installationPosition?: string;
  insertInto19InchCabinet?: boolean;
  terminalElement?: boolean;
  advancedEnvironmentalConditions?: boolean;

  //all data, only used in request
  motionEnable?: boolean;
  siplusCatalog?: boolean;
  simaticCatalog?: boolean;
  rotationAxisNumber?: number;
  positionAxisNumber?: number;
}
