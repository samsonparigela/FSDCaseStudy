import Delete from "./Delete";
import GetAll from "../../CustomerAccount/ViewAllBranches";
import GetByID from "./GetByID";
import Add from "./Add";

export default function Branches(){
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
        </div>
    )
}