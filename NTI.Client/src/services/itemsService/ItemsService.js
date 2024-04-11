import { BaseService } from "../baseService/baseService";


export class ItemsService extends BaseService {

    constructor(){
        super('/items')
    }

}