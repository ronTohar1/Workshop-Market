import Store from "../Store";
import Restriction from "./Restriction";

class AfterHour extends Restriction {
    hour: number;
    constructor(hour: number) {
        super("AfterHourRestriction");
        this.hour = hour;
    }
    public toString = () : string => {
        return `The store does not suffice it's purchase services after: ${this.hour}:00`;
    }
  }
  
  export default AfterHour