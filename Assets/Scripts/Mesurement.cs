using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesurement
{
    static public Vector3 MesureNormal(Transform tra, Collision collision, LayerMask mask)
    {
        Vector3 normal = Vector3.up;
        Vector3 centerPos = Vector3.zero, UpPos = Vector3.zero, BackPos = Vector3.zero;
        Vector3 contact = collision.contacts[collision.contacts.Length - 1].point;
        RaycastHit hit;
        if (Physics.Raycast(tra.position, contact - tra.position, out hit, 5f, mask))
        {
            centerPos = hit.point;
        }
        if (Physics.Raycast(tra.position - tra.right * 0.5f, contact - tra.position, out hit, 5f, mask))
        {
            BackPos = hit.point;
        }
        if (Physics.Raycast(tra.position + tra.up * 0.5f, contact - tra.position, out hit, 5f, mask))
        {
            UpPos = hit.point;
        }

        Vector3 dir1 = BackPos - centerPos;
        Vector3 dir2 = UpPos - centerPos;
        if (dir1 != Vector3.zero || dir2 != Vector3.zero)
        {
            normal = Vector3.Cross(dir2, dir1).normalized;
        }
        if (normal.y < 0 && (contact.y - tra.position.y) < 0)
        {
            normal = Vector3.Cross(dir1, dir2).normalized;
        }
        if (normal == Vector3.zero)
        {
            normal = Vector3.up;
        }
        return normal;
    }
    static public Vector3 MesureDirection(Transform tra, Collision collision, LayerMask mask, Vector3 direction)
    {
        Vector3 dir = tra.right;
        Vector3 centerPos = Vector3.zero, BackPos = Vector3.zero, ForwardPos = Vector3.zero;
        Vector3 contact = collision.contacts[collision.contacts.Length - 1].point;
        RaycastHit hit;
        if (Physics.Raycast(tra.position, direction, out hit, 5f, mask))
        {
            centerPos = hit.point;
        }
        if (Physics.Raycast(tra.position - tra.right * 0.5f, direction, out hit, 5f, mask))
        {
            BackPos = hit.point;
        }
        if (Physics.Raycast(tra.position + tra.right * 0.5f, direction, out hit, 5f, mask))
        {
            ForwardPos = hit.point;
        }
        if (centerPos != Vector3.zero && BackPos != Vector3.zero && ForwardPos != Vector3.zero)
        {
            Vector3 dir1 = centerPos - BackPos;
            Vector3 dir2 = ForwardPos - centerPos;
            dir = ((dir1 + dir2) * 0.5f).normalized;
            dir = new Vector3(dir.x, 0, dir.z).normalized;
        }

        Debug.DrawRay(tra.position, direction, Color.cyan);
        Debug.DrawRay(tra.position - tra.right * 0.5f, direction, Color.cyan);
        Debug.DrawRay(tra.position + tra.right * 0.5f, direction, Color.cyan);

        Debug.DrawRay(tra.position, dir * 5, Color.red);
        return dir;
    }
    static public Vector3 RayContactPoint(Vector3 start, Vector3 dir, LayerMask mask)
    {
        Vector3 contactPoint = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit, 5f, mask))
        {
            contactPoint = hit.point;
        }
        return contactPoint;
    }
}
