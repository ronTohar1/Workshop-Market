import Restriction from "./Restriction";

class DateRestriction extends Restriction {
    year : number;
    month : number;
    day : number;
    constructor(
        year : number,
        month : number,
        day : number
    ) {
        super();
        this.year = year;
        this.month = month;
        this.day = day;
    }
  }
  
  export default DateRestriction