import Restriction from "./Restriction";

class BeforeHour extends Restriction {
    hour: number;
    constructor(hour: number) {
        super("BeforeHourRestriction");
        this.hour = hour;
    }
    public toString = () : string => {
        return `The store does suffice it's purchase services before: ${this.hour}:00`;
    }
  }
  
  export default BeforeHour