import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "projectDataSourceDescription"
})
export class ProjectDataSourceDescriptionPipe implements PipeTransform {
  transform(value: any, args?: any): any {
    return value.map(val => val.description);
  }
}
