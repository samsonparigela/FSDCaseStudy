import Delete from "./Delete";
import GetAll from "../../CustomerAccount/ViewAllBanks";
import GetByID from "./GetByID";
import Add from "./Add";
import Update from "./Update"

export default function Banks(){
    return(
        <div>
            <div style={{ display: 'flex' ,width: '100%', backgroundColor: 'lightblue'}}>
            <Add />
            <Delete />
            </div>            
            <div style={{ display: 'flex' ,width: '100%', backgroundColor: 'lightblue'}}>
            <GetAll />

            <GetByID />
            
            </div>
            <div style={{ display: 'flex' }}>
            <Update />
            </div> 
        </div>
    )
}